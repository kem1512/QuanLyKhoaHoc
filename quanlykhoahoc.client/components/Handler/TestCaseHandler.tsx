"use client";

import { Grid, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  ITestCaseMapping,
  TestCaseClient,
  TestCaseCreate,
  TestCaseUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";
import {
  CertificateSelect,
  PracticeSelect,
  ProgramingLanguageSelect,
} from "../Helper/AppSelect";

export default function TestCaseHandler({ id }: { id?: number }) {
  const TestCaseService = new TestCaseClient();

  const { data } = useSWR(`/api/testCase/${id}`, () =>
    TestCaseService.getEntity(id)
  );

  const [testCase, setTestCase] = useState<ITestCaseMapping>({
    input: "",
    output: "",
  });

  useEffect(() => {
    if (data) setTestCase(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Input"
            placeholder="Nhập Input"
            value={testCase.input}
            onChange={(e) =>
              setTestCase((prev) => ({ ...prev, input: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Output"
            placeholder="Nhập Output"
            value={testCase.output}
            onChange={(e) =>
              setTestCase((prev) => ({ ...prev, output: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <ProgramingLanguageSelect
            onChange={(e) =>
              setTestCase((prev) => ({ ...prev, programingLanguageId: e }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <PracticeSelect
            onChange={(e) =>
              setTestCase((prev) => ({ ...prev, practiceId: e }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                return id
                  ? TestCaseService.updateEntity(
                      testCase.id,
                      testCase as TestCaseUpdate
                    )
                  : TestCaseService.createEntity(testCase as TestCaseCreate);
              }, `${id ? "Sửa" : "Thêm"} Thành Công`)
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
