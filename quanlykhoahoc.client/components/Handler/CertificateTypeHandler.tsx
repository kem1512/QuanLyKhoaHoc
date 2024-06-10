"use client";

import { Grid, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  ICertificateTypeMapping,
  CertificateTypeClient,
  CertificateTypeCreate,
  CertificateTypeUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";

export default function CertificateTypeHandler({ id }: { id?: number }) {
  const CertificateTypeService = new CertificateTypeClient();

  const { data } = useSWR(`/api/certificateType/${id}`, () =>
    CertificateTypeService.getEntity(id)
  );

  const [certificateType, setCertificateType] =
    useState<ICertificateTypeMapping>({
      name: "",
    });

  useEffect(() => {
    if (data) setCertificateType(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={6}>
          <TextInput
            label="Tên Chứng Chỉ"
            placeholder="Nhập Tên Chứng Chỉ"
            value={certificateType.name}
            onChange={(e) =>
              setCertificateType((prev) => ({ ...prev, name: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={6}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                return id
                  ? CertificateTypeService.updateEntity(
                      certificateType.id,
                      certificateType as CertificateTypeUpdate
                    )
                  : CertificateTypeService.createEntity(
                      certificateType as CertificateTypeCreate
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
