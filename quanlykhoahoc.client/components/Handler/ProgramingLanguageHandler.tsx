"use client";

import { Grid, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  IProgramingLanguageMapping,
  ProgramingLanguageClient,
  ProgramingLanguageCreate,
  ProgramingLanguageUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";

export default function ProgramingLanguageHandler({ id }: { id?: number }) {
  const ProgramingLanguageService = new ProgramingLanguageClient();

  const { data } = useSWR(`/api/programingLanguage/${id}`, () =>
    ProgramingLanguageService.getEntity(id)
  );

  const [programingLanguage, setProgramingLanguage] =
    useState<IProgramingLanguageMapping>({
      languageName: "",
    });

  useEffect(() => {
    if (data) setProgramingLanguage(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col>
          <TextInput
            label="Tên Chủ Đề"
            placeholder="Nhập Tên Chủ Đề"
            value={programingLanguage.languageName}
            onChange={(e) =>
              setProgramingLanguage((prev) => ({
                ...prev,
                languageName: e.target.value,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                id
                  ? ProgramingLanguageService.updateEntity(
                      programingLanguage.id,
                      programingLanguage as ProgramingLanguageUpdate
                    )
                  : ProgramingLanguageService.createEntity(
                      programingLanguage as ProgramingLanguageCreate
                    );
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
